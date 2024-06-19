import { StringFilter } from "../../util/StringFilter";
import { NotificationWhereUniqueInput } from "../notification/NotificationWhereUniqueInput";

export type DeliveryWhereInput = {
  id?: StringFilter;
  notification?: NotificationWhereUniqueInput;
};
