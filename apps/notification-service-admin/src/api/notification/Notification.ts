import { Delivery } from "../delivery/Delivery";

export type Notification = {
  createdAt: Date;
  date: Date | null;
  deliveries?: Array<Delivery>;
  id: string;
  message: string | null;
  updatedAt: Date;
};
