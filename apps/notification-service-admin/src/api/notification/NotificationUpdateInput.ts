import { DeliveryUpdateManyWithoutNotificationsInput } from "./DeliveryUpdateManyWithoutNotificationsInput";

export type NotificationUpdateInput = {
  date?: Date | null;
  deliveries?: DeliveryUpdateManyWithoutNotificationsInput;
  message?: string | null;
};
