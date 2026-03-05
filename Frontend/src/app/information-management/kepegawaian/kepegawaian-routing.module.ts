import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'

import { AuthGuard } from '@core/security/auth.guard'

import { KepegawaianComponent } from './kepegawaian.component'

const routes: Routes = [
  {
    path: '',
    component: KepegawaianComponent,
    data: { claimType: 'kepegawaian_view_documents' },
    canActivate: [AuthGuard],
  },
]

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class KepegawaianRoutingModule {}
