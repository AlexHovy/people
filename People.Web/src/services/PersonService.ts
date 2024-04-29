import { FileDto } from "../dtos/FileDto";
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

  async getProfilePicture(id: string): Promise<FileDto | undefined> {
    try {
      const response = await axiosInstance.get<FileDto>(
        `/person/GetProfilePicture/${id}`
      );
      return response.data;
    } catch (error) {
      handleError(error);
    }
  }

  async find(query: string): Promise<PersonDto[]> {
    try {
      const response = await axiosInstance.get<PersonDto[]>(
        `/person/Search/${query}`
      );
      return response.data;
    } catch (error) {
      handleError(error);
    }
    return [];
  }

  async create(person: PersonDto): Promise<PersonDto | undefined> {
    try {
      const response = await axiosInstance.post<PersonDto>("/person", person);
      return response.data;
    } catch (error) {
      handleError(error);
    }
  }

  async uploadProfilePicture(
    personId: string,
    fileDto: FileDto
  ): Promise<FileDto | undefined> {
    try {
      const response = await axiosInstance.post<FileDto>(
        `/person/UploadProfilePicture/${personId}`,
        fileDto
      );
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
