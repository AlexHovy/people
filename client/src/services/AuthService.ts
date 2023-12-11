import { useNavigate } from "react-router-dom";
import { LocalStorageService } from "./LocalStorageService";
import { LocalStorageKeys } from "../constants/LocalStorageKeys";
import { handleError } from "../utils/ErrorHandlerUtil";
import { NavigationPages } from "../constants/NavigationPages";
import { NotificationService } from "./NotificationService";

export class AuthService {
  private navigate = useNavigate();

  signOut = async () => {
    try {
      LocalStorageService.remove(LocalStorageKeys.Token);
      this.navigate(NavigationPages.Home);
    } catch (error: any) {
      handleError(error);
    }
  };

  signIn = async (username: string, password: string) => {
    try {
      
      this.navigate(NavigationPages.ManagePeople);
    } catch (error: any) {
      handleError(error);
    }
  };
}
