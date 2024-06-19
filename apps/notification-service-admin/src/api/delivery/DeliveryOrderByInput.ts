import { SortOrder } from "../../util/SortOrder";

export type DeliveryOrderByInput = {
  createdAt?: SortOrder;
  id?: SortOrder;
  notificationId?: SortOrder;
  updatedAt?: SortOrder;
};
