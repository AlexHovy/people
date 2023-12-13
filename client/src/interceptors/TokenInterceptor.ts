import axios, { InternalAxiosRequestConfig } from "axios";
import { SettingsConfig } from "../config/SettingsConfig";
import { AuthService } from "../services/AuthService";
import { LocalStorageService } from "../services/LocalStorageService";
import { TokenDto } from "../dtos/TokenDto";
import { LocalStorageKeys } from "../constants/LocalStorageKeys";

const axiosInstance = axios.create({
  baseURL: SettingsConfig.getBaseApiUrl(),
});

axiosInstance.interceptors.request.use(
  async (config: InternalAxiosRequestConfig) => {
    const authService = new AuthService();
    if (authService.isAuthenticated()) {
      const token = authService.getToken();
      config.headers = config.headers || {};
      config.headers["Authorization"] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default axiosInstance;
