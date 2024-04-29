import { CityDto } from "../dtos/CityDto";
import axiosInstance from "../interceptors/TokenInterceptor";
import { handleError } from "../utils/ErrorHandlerUtil";

export class CityService {
  async get(): Promise<CityDto[]> {
    try {
      const response = await axiosInstance.get<CityDto[]>("/city");
      return response.data;
    } catch (error) {
      handleError(error);
    }
    return [];
  }

  async getById(id: string): Promise<CityDto | undefined> {
    try {
      const response = await axiosInstance.get<CityDto>(`/city/${id}`);
      return response.data;
    } catch (error) {
      handleError(error);
    }
  }

  async getByCountryId(countrId: string): Promise<CityDto[]> {
    try {
      const response = await axiosInstance.get<CityDto[]>(`/city/ByCountryId/${countrId}`);
      return response.data;
    } catch (error) {
      handleError(error);
    }
    return [];
  }
}
