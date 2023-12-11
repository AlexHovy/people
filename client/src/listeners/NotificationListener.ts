import { useEffect } from "react";
import { useNotification } from "../contexts/NotificationContext";
import { notificationEmitter } from "../config/EventsConfig";
import { NotificationTypes } from "../constants/NotificationTypes";
import { EventTypes } from "../constants/EventTypes";

const NotificationListener = () => {
  const { showMessage } = useNotification();

  useEffect(() => {
    const handleErrorNotification = (message: string) => {
      showMessage(message, NotificationTypes.ERROR);
    };

    const handleSuccessNotification = (message: string) => {
      showMessage(message, NotificationTypes.SUCCESS);
    };

    const handleDefaultNotification = (message: string) => {
      showMessage(message, NotificationTypes.DEFAULT);
    };

    notificationEmitter.on(
      EventTypes.NOTIFICATION_ERROR,
      handleErrorNotification
    );
    notificationEmitter.on(
      EventTypes.NOTIFICATION_SUCCESS,
      handleSuccessNotification
    );
    notificationEmitter.on(
      EventTypes.NOTIFICATION_DEFAULT,
      handleDefaultNotification
    );

    return () => {
      notificationEmitter.off(
        EventTypes.NOTIFICATION_ERROR,
        handleErrorNotification
      );
      notificationEmitter.off(
        EventTypes.NOTIFICATION_SUCCESS,
        handleSuccessNotification
      );
      notificationEmitter.off(
        EventTypes.NOTIFICATION_DEFAULT,
        handleDefaultNotification
      );
    };
  }, [showMessage]);

  return null;
};

export default NotificationListener;
