import { IOrderItemCheckoutModel } from "./order-item-checkout.model";
import { IOrderItemModel } from "./order-item.model";

export interface IOrderPostModel {
    formationDate: Date;
    orderItems: IOrderItemModel[];
}