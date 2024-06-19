import * as React from "react";
import {
  Create,
  SimpleForm,
  CreateProps,
  ReferenceInput,
  SelectInput,
} from "react-admin";
import { NotificationTitle } from "../notification/NotificationTitle";

export const DeliveryCreate = (props: CreateProps): React.ReactElement => {
  return (
    <Create {...props}>
      <SimpleForm>
        <ReferenceInput
          source="notification.id"
          reference="Notification"
          label="Notification"
        >
          <SelectInput optionText={NotificationTitle} />
        </ReferenceInput>
      </SimpleForm>
    </Create>
  );
};
