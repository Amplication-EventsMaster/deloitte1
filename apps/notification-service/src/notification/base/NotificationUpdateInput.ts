/*
------------------------------------------------------------------------------ 
This code was generated by Amplication. 
 
Changes to this file will be lost if the code is regenerated. 

There are other ways to to customize your code, see this doc to learn more
https://docs.amplication.com/how-to/custom-code

------------------------------------------------------------------------------
  */
import { InputType, Field } from "@nestjs/graphql";
import { ApiProperty } from "@nestjs/swagger";
import { IsDate, IsOptional, ValidateNested, IsString } from "class-validator";
import { Type } from "class-transformer";
import { DeliveryUpdateManyWithoutNotificationsInput } from "./DeliveryUpdateManyWithoutNotificationsInput";

@InputType()
class NotificationUpdateInput {
  @ApiProperty({
    required: false,
  })
  @IsDate()
  @Type(() => Date)
  @IsOptional()
  @Field(() => Date, {
    nullable: true,
  })
  date?: Date | null;

  @ApiProperty({
    required: false,
    type: () => DeliveryUpdateManyWithoutNotificationsInput,
  })
  @ValidateNested()
  @Type(() => DeliveryUpdateManyWithoutNotificationsInput)
  @IsOptional()
  @Field(() => DeliveryUpdateManyWithoutNotificationsInput, {
    nullable: true,
  })
  deliveries?: DeliveryUpdateManyWithoutNotificationsInput;

  @ApiProperty({
    required: false,
    type: String,
  })
  @IsString()
  @IsOptional()
  @Field(() => String, {
    nullable: true,
  })
  message?: string | null;
}

export { NotificationUpdateInput as NotificationUpdateInput };
