import { Component, EventEmitter, Input, Output } from '@angular/core';
import { IProductModel } from '../../models/product.model';

@Component({
    selector: 'app-product-card',
    templateUrl: './product-card.component.html',
    styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent {

    isAddedToCart = false;

    @Input() product: IProductModel;
    @Input() isDollarCurrency: boolean = true;

    @Output() addToCartEvent : EventEmitter<IProductModel> = new EventEmitter<IProductModel>();
    @Output() redirectToCartEvent : EventEmitter<any> = new EventEmitter();
    

    constructor() { }


    addToCart() {
        this.isAddedToCart = true;
        this.addToCartEvent?.emit(this.product);
    }

    turnCurrency() {
        this.isDollarCurrency = !this.isDollarCurrency;
    }

    redirectToCart() {
        this.redirectToCartEvent?.emit();
    }
}
