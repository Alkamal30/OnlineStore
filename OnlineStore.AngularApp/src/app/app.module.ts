import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NZ_I18N, en_US } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IconsProviderModule } from './icons-provider.module';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { ProductCardComponent } from './pages/product-card/product-card.component';
import { LayoutComponent } from './pages/layout/layout.component';
import { NzImageModule } from 'ng-zorro-antd/image';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { LogInComponent } from './pages/log-in/log-in.component';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzPopoverModule } from 'ng-zorro-antd/popover';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { CartComponent } from './pages/cart/cart.component';
import { OrdersComponent } from './pages/orders/orders.component';
import { CartCardComponent } from './pages/cart-card/cart-card.component';
import { NzTypographyModule } from 'ng-zorro-antd/typography'; 
import { NzAffixModule } from 'ng-zorro-antd/affix';
import { MyOrdersComponent } from './pages/my-orders/my-orders.component';
import { OrderComponent } from './pages/order/order.component';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { AuthorizationInterceptor } from './services/authorization.interceptor';
import { AuthorizationService } from './services/authorization.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { ErrorsInterceptor } from './services/errors.interceptor';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { NzTableModule } from 'ng-zorro-antd/table';
import { ProductsComponent } from './pages/products/products.component';
import { FilterProductsPipe } from './pipes/filter-products.pipe';
import { LayoutModule } from '@angular/cdk/layout';
import { HomeComponent } from './pages/home/home.component';
import { EditableProductComponent } from './pages/editable-product/editable-product.component';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';


registerLocaleData(en);


@NgModule({
  declarations: [
    AppComponent,
    ProductCardComponent,
    LayoutComponent,
    LogInComponent,
    HomeComponent,
    CartComponent,
    OrdersComponent,
    CartCardComponent,
    MyOrdersComponent,
    OrderComponent,
    ProductsComponent,
    FilterProductsPipe,
    EditableProductComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    LayoutModule,
    IconsProviderModule,
    NzLayoutModule,
    NzMenuModule,
    NzButtonModule,
    NzCardModule,
    NzIconModule,
    NzListModule,
    NzGridModule,
    NzSpaceModule,
    NzLayoutModule,
    NzImageModule,
    NzBreadCrumbModule,
    NzDividerModule,
    NzFormModule,
    NzInputModule,
    NzPopoverModule,
    NzPopconfirmModule,
    NzModalModule,
    NzInputNumberModule,
    NzTypographyModule,
    NzAffixModule,
    NzEmptyModule,
    NzTableModule,
    NzToolTipModule,
    NzCheckboxModule
  ],
  providers: [
    { provide: NZ_I18N, useValue: en_US },

    AuthorizationService,
    NzNotificationService,
    {
        provide: HTTP_INTERCEPTORS,
        useClass: AuthorizationInterceptor,
        multi: true
    },
    {
        provide: HTTP_INTERCEPTORS,
        useClass: ErrorsInterceptor,
        multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
