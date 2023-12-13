import { LocalStorageService } from "./LocalStorageService";
import { LocalStorageKeys } from "../constants/LocalStorageKeys";
import { handleError } from "../utils/ErrorHandlerUtil";
import axiosInstance from "../interceptors/TokenInterceptor";
import { TokenDto } from "../dtos/TokenDto";
import { LoginDto } from "../dtos/LoginDto";

export class AuthService {
  signOut = async () => {
    try {
      LocalStorageService.remove(LocalStorageKeys.Token);
    } catch (error: any) {
      handleError(error);
    }
  };

  signIn = async (loginDto: LoginDto) => {
    try {
      const response = await axiosInstance.post<TokenDto>(
        `/auth/login`,
        loginDto,
        {
          withCredentials: true,
        }
      );
      if (!response) return;

      LocalStorageService.set<TokenDto>(LocalStorageKeys.Token, response.data);
    } catch (error: any) {
      handleError(error);
    }
  };

  getToken(): TokenDto | null {
    const token = LocalStorageService.get<TokenDto>(LocalStorageKeys.Token);
    return token;
  }

  isAuthenticated(): boolean {
    const token = this.getToken();
    return !!token && !this.isTokenExpired();
  }

  isTokenExpired(): boolean {
    const token = this.getToken();
    if (token && token.expiresAt) {
      return token.expiresAt < new Date();
    }
    return true;
  }
}
