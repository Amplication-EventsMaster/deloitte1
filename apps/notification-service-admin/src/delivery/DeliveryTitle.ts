import { Delivery as TDelivery } from "../api/delivery/Delivery";

export const DELIVERY_TITLE_FIELD = "id";

export const DeliveryTitle = (record: TDelivery): string => {
  return record.id?.toString() || String(record.id);
};
