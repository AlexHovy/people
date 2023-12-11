import { NotificationService } from "../services/NotificationService";

export const handleError = (error: any) => {
  // TODO: log errors with service insights
  console.error(error);

  NotificationService.showErrorNotification(error.message);
};
