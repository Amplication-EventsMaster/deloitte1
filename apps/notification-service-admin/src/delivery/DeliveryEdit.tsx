import * as React from "react";
import {
  Edit,
  SimpleForm,
  EditProps,
  ReferenceInput,
  SelectInput,
} from "react-admin";
import { NotificationTitle } from "../notification/NotificationTitle";

export const DeliveryEdit = (props: EditProps): React.ReactElement => {
  return (
    <Edit {...props}>
      <SimpleForm>
        <ReferenceInput
          source="notification.id"
          reference="Notification"
          label="Notification"
        >
          <SelectInput optionText={NotificationTitle} />
        </ReferenceInput>
      </SimpleForm>
    </Edit>
  );
};
