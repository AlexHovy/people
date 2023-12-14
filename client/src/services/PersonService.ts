import { PersonDto } from "../dtos/PersonDto";
import axiosInstance from "../interceptors/TokenInterceptor";
import { handleError } from "../utils/ErrorHandlerUtil";

export class PersonService {
  async get(): Promise<PersonDto[]> {
    try {
      const response = await axiosInstance.get<PersonDto[]>("/person");
      return response.data;
    } catch (error) {
      handleError(error);
    }
    return [];
  }

  async getById(id: string): Promise<PersonDto | undefined> {
    try {
      const response = await axiosInstance.get<PersonDto>(`/person/${id}`);
      return response.data;
    } catch (error) {
      handleError(error);
    }
  }

  async find(query: string): Promise<PersonDto | undefined> {
    try {
      const response = await axiosInstance.get<PersonDto>(`/person/search/${query}`);
      return response.data;
    } catch (error) {
      handleError(error);
    }
  }

  async create(person: PersonDto): Promise<PersonDto | undefined> {
    try {
      const response = await axiosInstance.post<PersonDto>("/person", person);
      return response.data;
    } catch (error) {
      handleError(error);
    }
  }

  async update(person: PersonDto): Promise<PersonDto | undefined> {
    try {
      const response = await axiosInstance.put<PersonDto>(`/person`, person);
      return response.data;
    } catch (error) {
      handleError(error);
    }
  }

  async delete(id: string): Promise<void> {
    try {
      await axiosInstance.delete(`/person/${id}`);
    } catch (error) {
      handleError(error);
    }
  }
}
