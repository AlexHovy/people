import { CountryDto } from "../dtos/CountryDto";
import axiosInstance from "../interceptors/TokenInterceptor";
import { handleError } from "../utils/ErrorHandlerUtil";

export class CountryService {
  async get(): Promise<CountryDto[]> {
    try {
      const response = await axiosInstance.get<CountryDto[]>("/country");
      return response.data;
    } catch (error) {
      handleError(error);
    }
    return [];
  }

  async getById(id: string): Promise<CountryDto | undefined> {
    try {
      const response = await axiosInstance.get<CountryDto>(`/country/${id}`);
      return response.data;
    } catch (error) {
      handleError(error);
    }
  }
}
