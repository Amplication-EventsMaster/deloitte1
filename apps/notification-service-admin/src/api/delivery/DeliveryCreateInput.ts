import { NotificationWhereUniqueInput } from "../notification/NotificationWhereUniqueInput";

export type DeliveryCreateInput = {
  notification?: NotificationWhereUniqueInput | null;
};
