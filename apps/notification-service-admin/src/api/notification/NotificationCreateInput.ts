import { DeliveryCreateNestedManyWithoutNotificationsInput } from "./DeliveryCreateNestedManyWithoutNotificationsInput";

export type NotificationCreateInput = {
  date?: Date | null;
  deliveries?: DeliveryCreateNestedManyWithoutNotificationsInput;
  message?: string | null;
};
