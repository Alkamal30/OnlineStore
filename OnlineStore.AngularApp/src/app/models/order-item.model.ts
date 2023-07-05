import { IProductModel } from "./product.model";
import { IUserModel } from "./user.model";

export interface IOrderItemModel {
    id: number;
    product: IProductModel;
    count: number;
}