import axios, { InternalAxiosRequestConfig } from "axios";
import { SettingsConfig } from "../config/SettingsConfig";
import { AuthService } from "../services/AuthService";

const axiosInstance = axios.create({
  baseURL: SettingsConfig.getBaseApiUrl(),
});

axiosInstance.interceptors.request.use(
  async (config: InternalAxiosRequestConfig) => {
    const authService = new AuthService();
    if (authService.isAuthenticated()) {
      const tokenDto = authService.getToken();
      config.headers = config.headers || {};
      config.headers["Authorization"] = `Bearer ${tokenDto?.token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default axiosInstance;
