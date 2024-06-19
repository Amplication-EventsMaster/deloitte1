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
import { NotificationWhereUniqueInput } from "../../notification/base/NotificationWhereUniqueInput";
import { ValidateNested, IsOptional } from "class-validator";
import { Type } from "class-transformer";

@InputType()
class DeliveryUpdateInput {
  @ApiProperty({
    required: false,
    type: () => NotificationWhereUniqueInput,
  })
  @ValidateNested()
  @Type(() => NotificationWhereUniqueInput)
  @IsOptional()
  @Field(() => NotificationWhereUniqueInput, {
    nullable: true,
  })
  notification?: NotificationWhereUniqueInput | null;
}

export { DeliveryUpdateInput as DeliveryUpdateInput };
