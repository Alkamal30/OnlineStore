import { IOrderItemCheckoutModel } from "./order-item-checkout.model";

export interface IOrderCheckoutModel {
    formationDate: Date;
    orderItems: IOrderItemCheckoutModel[];
}