/*
------------------------------------------------------------------------------ 
This code was generated by Amplication. 
 
Changes to this file will be lost if the code is regenerated. 

There are other ways to to customize your code, see this doc to learn more
https://docs.amplication.com/how-to/custom-code

------------------------------------------------------------------------------
  */
import * as common from "@nestjs/common";
import * as swagger from "@nestjs/swagger";
import { isRecordNotFoundError } from "../../prisma.util";
import * as errors from "../../errors";
import { Request } from "express";
import { plainToClass } from "class-transformer";
import { ApiNestedQuery } from "../../decorators/api-nested-query.decorator";
import { NotificationService } from "../notification.service";
import { NotificationCreateInput } from "./NotificationCreateInput";
import { Notification } from "./Notification";
import { NotificationFindManyArgs } from "./NotificationFindManyArgs";
import { NotificationWhereUniqueInput } from "./NotificationWhereUniqueInput";
import { NotificationUpdateInput } from "./NotificationUpdateInput";
import { DeliveryFindManyArgs } from "../../delivery/base/DeliveryFindManyArgs";
import { Delivery } from "../../delivery/base/Delivery";
import { DeliveryWhereUniqueInput } from "../../delivery/base/DeliveryWhereUniqueInput";

export class NotificationControllerBase {
  constructor(protected readonly service: NotificationService) {}
  @common.Post()
  @swagger.ApiCreatedResponse({ type: Notification })
  async createNotification(
    @common.Body() data: NotificationCreateInput
  ): Promise<Notification> {
    return await this.service.createNotification({
      data: data,
      select: {
        createdAt: true,
        date: true,
        id: true,
        message: true,
        updatedAt: true,
      },
    });
  }

  @common.Get()
  @swagger.ApiOkResponse({ type: [Notification] })
  @ApiNestedQuery(NotificationFindManyArgs)
  async notifications(@common.Req() request: Request): Promise<Notification[]> {
    const args = plainToClass(NotificationFindManyArgs, request.query);
    return this.service.notifications({
      ...args,
      select: {
        createdAt: true,
        date: true,
        id: true,
        message: true,
        updatedAt: true,
      },
    });
  }

  @common.Get("/:id")
  @swagger.ApiOkResponse({ type: Notification })
  @swagger.ApiNotFoundResponse({ type: errors.NotFoundException })
  async notification(
    @common.Param() params: NotificationWhereUniqueInput
  ): Promise<Notification | null> {
    const result = await this.service.notification({
      where: params,
      select: {
        createdAt: true,
        date: true,
        id: true,
        message: true,
        updatedAt: true,
      },
    });
    if (result === null) {
      throw new errors.NotFoundException(
        `No resource was found for ${JSON.stringify(params)}`
      );
    }
    return result;
  }

  @common.Patch("/:id")
  @swagger.ApiOkResponse({ type: Notification })
  @swagger.ApiNotFoundResponse({ type: errors.NotFoundException })
  async updateNotification(
    @common.Param() params: NotificationWhereUniqueInput,
    @common.Body() data: NotificationUpdateInput
  ): Promise<Notification | null> {
    try {
      return await this.service.updateNotification({
        where: params,
        data: data,
        select: {
          createdAt: true,
          date: true,
          id: true,
          message: true,
          updatedAt: true,
        },
      });
    } catch (error) {
      if (isRecordNotFoundError(error)) {
        throw new errors.NotFoundException(
          `No resource was found for ${JSON.stringify(params)}`
        );
      }
      throw error;
    }
  }

  @common.Delete("/:id")
  @swagger.ApiOkResponse({ type: Notification })
  @swagger.ApiNotFoundResponse({ type: errors.NotFoundException })
  async deleteNotification(
    @common.Param() params: NotificationWhereUniqueInput
  ): Promise<Notification | null> {
    try {
      return await this.service.deleteNotification({
        where: params,
        select: {
          createdAt: true,
          date: true,
          id: true,
          message: true,
          updatedAt: true,
        },
      });
    } catch (error) {
      if (isRecordNotFoundError(error)) {
        throw new errors.NotFoundException(
          `No resource was found for ${JSON.stringify(params)}`
        );
      }
      throw error;
    }
  }

  @common.Get("/:id/deliveries")
  @ApiNestedQuery(DeliveryFindManyArgs)
  async findDeliveries(
    @common.Req() request: Request,
    @common.Param() params: NotificationWhereUniqueInput
  ): Promise<Delivery[]> {
    const query = plainToClass(DeliveryFindManyArgs, request.query);
    const results = await this.service.findDeliveries(params.id, {
      ...query,
      select: {
        createdAt: true,
        id: true,

        notification: {
          select: {
            id: true,
          },
        },

        updatedAt: true,
      },
    });
    if (results === null) {
      throw new errors.NotFoundException(
        `No resource was found for ${JSON.stringify(params)}`
      );
    }
    return results;
  }

  @common.Post("/:id/deliveries")
  async connectDeliveries(
    @common.Param() params: NotificationWhereUniqueInput,
    @common.Body() body: DeliveryWhereUniqueInput[]
  ): Promise<void> {
    const data = {
      deliveries: {
        connect: body,
      },
    };
    await this.service.updateNotification({
      where: params,
      data,
      select: { id: true },
    });
  }

  @common.Patch("/:id/deliveries")
  async updateDeliveries(
    @common.Param() params: NotificationWhereUniqueInput,
    @common.Body() body: DeliveryWhereUniqueInput[]
  ): Promise<void> {
    const data = {
      deliveries: {
        set: body,
      },
    };
    await this.service.updateNotification({
      where: params,
      data,
      select: { id: true },
    });
  }

  @common.Delete("/:id/deliveries")
  async disconnectDeliveries(
    @common.Param() params: NotificationWhereUniqueInput,
    @common.Body() body: DeliveryWhereUniqueInput[]
  ): Promise<void> {
    const data = {
      deliveries: {
        disconnect: body,
      },
    };
    await this.service.updateNotification({
      where: params,
      data,
      select: { id: true },
    });
  }
}
