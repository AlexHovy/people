import { PersonDto } from "../dtos/PersonDto";
import axiosInstance from "../interceptors/TokenInterceptor";
import { handleError } from "../utils/ErrorHandlerUtil";
import { NotificationService } from "./NotificationService";

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

  async create(person: PersonDto): Promise<PersonDto | undefined> {
    try {
      const response = await axiosInstance.post<PersonDto>(
        "/person",
        person
      );
      NotificationService.showSuccessNotification(
        "Person created successfully!"
      );
      return response.data;
    } catch (error) {
      handleError(error);
    }
  }

  async update(person: PersonDto): Promise<PersonDto | undefined> {
    try {
      const response = await axiosInstance.put<PersonDto>(
        `/person/${person.id}`,
        person
      );
      NotificationService.showSuccessNotification(
        "Person updated successfully!"
      );
      return response.data;
    } catch (error) {
      handleError(error);
    }
  }

  async delete(id: string): Promise<void> {
    try {
      await axiosInstance.delete(`/person/${id}`);
      NotificationService.showSuccessNotification(
        "Person deleted successfully!"
      );
    } catch (error) {
      handleError(error);
    }
  }
}
