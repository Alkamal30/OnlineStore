import { Component, OnInit } from '@angular/core';
import { AuthorizationService } from '../../services/authorization.service';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';

@Component({
    selector: 'app-layout',
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.css'],
    providers: [ AuthorizationService ]
})
export class LayoutComponent implements OnInit {

    isCollapsed = false;
    isMobile = false;

    constructor(
        public breakpointObserver: BreakpointObserver,
        public authService: AuthorizationService
    ) { }

    ngOnInit(): void {
        this.breakpointObserver
            .observe(['(max-width: 900px)'])
            .subscribe(x => this.isMobile = x.matches);
    }

    toggleCollapsed() {
        this.isCollapsed = !this.isCollapsed;
    }
}
