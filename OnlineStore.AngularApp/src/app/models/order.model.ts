import { IOrderItemCheckoutModel } from "./order-item-checkout.model";
import { IOrderItemModel } from "./order-item.model";
import { IUserModel } from "./user.model";

export interface IOrderModel {
    id : number;
    customer: IUserModel;
    formationDate: Date;
    orderItems: IOrderItemModel[];
}