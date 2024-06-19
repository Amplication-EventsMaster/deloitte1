import { Notification } from "../notification/Notification";

export type Delivery = {
  createdAt: Date;
  id: string;
  notification?: Notification | null;
  updatedAt: Date;
};
