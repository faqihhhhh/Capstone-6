import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'

import { AuthGuard } from '@core/security/auth.guard'

import { PpmComponent } from './ppm.component'

const routes: Routes = [
  {
    path: '',
    component: PpmComponent,
    data: { claimType: 'ppm_view_documents' },
    canActivate: [AuthGuard],
  },
]

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PpmRoutingModule {}
