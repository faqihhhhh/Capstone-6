import { Component, OnInit } from '@angular/core'
import { Router } from '@angular/router'

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.scss'],
    standalone: false
})
export class SidebarComponent implements OnInit {
  activeMenu: string = '';

  constructor(private router: Router) {}

  ngOnInit(): void {}

  toggleMenu(menuName: string) {
    this.activeMenu = this.activeMenu === menuName ? '' : menuName;
  }

  addClass(event): void {
    event.target.className += ' showMenu'
  }

  removeClass(event): void {
    event.target.className = event.target.className.replace('showMenu', '')
  }
  onActiveUrl() {
    if (
      this.router.url === '/operations' ||
      this.router.url === '/screens' ||
      this.router.url === '/screen-operation'
    ) {
      return 'active'
    }
  }
}
