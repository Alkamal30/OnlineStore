import { IProductModel } from "./product.model";

export interface IOrderItemCheckoutModel {
    product: IProductModel;
    count: number;
}