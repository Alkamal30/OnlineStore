import { Component, EventEmitter, Inject, Input, Output, Renderer2 } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CookieService } from 'ngx-cookie-service';
import { IOrderItemModel } from 'src/app/models/order-item.model';
import { IProductModel } from 'src/app/models/product.model';
import { AuthorizationService } from 'src/app/services/authorization.service';
import { ProductsCrudService } from 'src/app/services/products.crud.service';
import { ProductCardComponent } from '../product-card/product-card.component';
import { DOCUMENT } from '@angular/common';
import { FallbackImage } from 'src/app/utilities/fallback-image';
import { switchMap } from 'rxjs';
import { BreakpointObserver } from '@angular/cdk/layout';

@Component({
    selector: 'app-products',
    templateUrl: './products.component.html',
    styleUrls: ['./products.component.css'],
    providers: [
        AuthorizationService, 
        ProductsCrudService,
        NzMessageService
    ]
})
export class ProductsComponent {

    constructor(
        public breakpointObserver: BreakpointObserver,
        public authService: AuthorizationService,
        private crudService: ProductsCrudService,
        private messageService: NzMessageService,
        private formBuilder: UntypedFormBuilder,
        private cookieService: CookieService,
        private router: Router
    ) { }


    filterValue: string = '';
    filterOptions = [
        { label: 'Title', value: 'Title', checked: true },
        { label: 'Description', value: 'Description' }
    ]

    products: IProductModel[] = [];
    oldProduct?: IProductModel;
    editableProductCard?: ProductCardComponent;

    shoppingCart: IOrderItemModel[] = [];

    isModalVisible: boolean = false;
    isModalEditable: boolean = false;
    validateForm!: UntypedFormGroup;
    modalTitle: string = "";
    modalProduct: IProductModel;

    
    ngOnInit() {
        this.updateProducts();

        this.shoppingCart = this.authService.userShoppingCart;

        this.validateForm = this.formBuilder.group({
            title: [null, [Validators.required]],
            price: [null, [Validators.required, Validators.min(0)]],
            description: [null, [Validators.maxLength(2000)]],
            image: [null, []]
        });
    }

    getSearchBarWidth() {
        let containerWidth = document
            .getElementsByClassName('search-bar-container')
            .item(0)?.clientWidth ?? 0;

        let cardWidth = document
            .getElementsByTagName(this.authService.isAdmin ? 'app-editable-product' : 'app-product-card')
            .item(0)?.clientWidth ?? 0;
        
        cardWidth += 20;

        return containerWidth - (containerWidth % cardWidth) - 20;
    }


    updateProducts() {
        this.crudService
            .getProducts()
            .subscribe(x => {
                this.products = x;
                this.products.sort((a, b) => {
                    if(a.id == b.id)
                        return 0;

                    return ((a?.id as number) > (b?.id as number)) ? 1 : -1;
                });
                console.log(x);
            });
    }

    add() {
        this.crudService
            .addProduct(this.modalProduct)
            .subscribe(x => {
                this.updateProducts();
                this.messageService.success('Successfully added!');
            });
    }


    addNewProduct() {
        this.isModalEditable = false;
        this.modalProduct = {
            id: 0,
            title: '',
            price: 0,
            description: '',
            image: ''
        };
        this.showModal('Add New Product')
    }

    startEditCard(product: IProductModel) {
        this.isModalEditable = true;
        this.modalProduct = {
            id: product.id,
            title: product.title,
            price: product.price,
            description: product.description,
            image: product.image
        };
        
        this.showModal('Edit Product')
    }

    save() {
        this.crudService
            .updateProduct(this.modalProduct)
            .subscribe(x => { 
                this.updateProducts();
                this.messageService.success('Successfully changed!')
            });
    }

    removeCard(product: IProductModel) {
        this.crudService
            .removeProduct(product.id as number)
            .subscribe(x => {
                this.updateProducts();
                this.messageService.success('Successfully removed!');
            });
    }


    addToCart(product: IProductModel) {
        this.shoppingCart.push({
            id: 0,
            product: { 
                id: product.id,
                title: product.title,
                price: product.price,
                description: product.description,
                image: product.image
            },
            count: 1
        });
        this.authService.userShoppingCart = this.shoppingCart;
        this.messageService.success('Successfully added to Cart!');
    }

    redirectToCart() {
        this.router.navigateByUrl('/cart');
    }



    showModal(title: string) {
        this.isModalVisible = true;
        this.modalTitle = title;
    }

    handleModalCancel() {
        this.isModalVisible = false;
    }

    handleModalOk() {
        if(this.validateForm.valid) {
            this.isModalVisible = false;

            if(this.isModalEditable)
                this.save();
            else this.add();

            return;
        }

        Object.values(this.validateForm.controls)
            .forEach(control => {
                if(control.invalid) {
                    control.markAsDirty();
                    control.updateValueAndValidity({onlySelf: true});
                }
            });
    }

    getFallbackImage(): string {
        return FallbackImage.Base64;
    }
}
