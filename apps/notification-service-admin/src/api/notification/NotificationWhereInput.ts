import { DateTimeNullableFilter } from "../../util/DateTimeNullableFilter";
import { DeliveryListRelationFilter } from "../delivery/DeliveryListRelationFilter";
import { StringFilter } from "../../util/StringFilter";
import { StringNullableFilter } from "../../util/StringNullableFilter";

export type NotificationWhereInput = {
  date?: DateTimeNullableFilter;
  deliveries?: DeliveryListRelationFilter;
  id?: StringFilter;
  message?: StringNullableFilter;
};
