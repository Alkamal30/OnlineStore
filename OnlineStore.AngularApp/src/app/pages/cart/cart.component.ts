import { Component, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import { IProductModel } from '../../models/product.model';
import { AuthorizationService } from '../../services/authorization.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CartCardComponent } from '../cart-card/cart-card.component';
import { IOrderModel } from '../../models/order.model';
import { IUserOrderPostModel } from '../../models/user-order-post.model';
import { OrdersService } from '../../services/orders.service';
import { IOrderPostModel } from 'src/app/models/order-post.model';
import { IOrderCheckoutModel } from 'src/app/models/order-checkout.model';
import { IOrderItemCheckoutModel } from 'src/app/models/order-item-checkout.model';
import { IOrderItemModel } from 'src/app/models/order-item.model';
import { BreakpointObserver } from '@angular/cdk/layout';


@Component({
    selector: 'cart',
    templateUrl: './cart.component.html',
    styleUrls: ['./cart.component.css'],
    providers: [ 
        AuthorizationService,
        NzMessageService,
        OrdersService
    ]
})
export class CartComponent implements OnInit {
    
    constructor(
        public breakpointObserver: BreakpointObserver,
        public authService: AuthorizationService,
        private messageService: NzMessageService,
        private ordersService: OrdersService
    ) { }

    cartItems: IOrderItemModel[] = [];

    ngOnInit(): void {
        this.cartItems = this.authService.userShoppingCart;
    }


    checkout() {
        this.ordersService.checkout({
            formationDate: new Date(),
            orderItems: this.cartItems
        });

        this.cartItems = [];
        this.updateCartCookie();
        this.messageService.success('Order successfully checkouted!');
    }

    removeCard(cartItem: IOrderItemModel) {
        this.cartItems.splice(
            this.cartItems.indexOf(cartItem),
            1
        );

        this.updateCartCookie();
        this.messageService.success('Successfully removed from Cart!');
    }

    onCountChanged(cartItem: IOrderItemModel) {
        this.updateCartCookie();
    }

    private updateCartCookie() {
        this.authService.userShoppingCart = this.cartItems
    }
}
