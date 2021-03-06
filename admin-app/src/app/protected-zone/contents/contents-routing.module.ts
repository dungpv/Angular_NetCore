import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { KnowledgeBasesComponent } from './knowledge-bases/knowledge-bases.component';
import { CommentsComponent } from './knowledge-bases/comments/comments.component';
import { ReportsComponent } from './knowledge-bases/reports/reports.component';
import { CategoriesComponent } from './categories/categories.component';
import { AuthGuard } from '@app/shared';
import { KnowledgeBasesDetailComponent } from './knowledge-bases/knowledge-bases-detail/knowledge-bases-detail.component';
import { SogdComponent } from './sogd/sogd.component';
import { PhonggdComponent } from './phonggd/phonggd.component';
import { TruongComponent } from './truong/truong.component';
import { LopComponent } from './lop/lop.component';

const routes: Routes = [
    {
        path: '',
        component: KnowledgeBasesComponent,
        data: {
            functionCode: 'CONTENT'
        },
        canActivate: [AuthGuard]
    },
    {
        path: 'knowledge-bases',
        component: KnowledgeBasesComponent,
        data: {
            functionCode: 'CONTENT_KNOWLEDGEBASE'
        },
        canActivate: [AuthGuard]
    },
    {
        path: 'knowledge-bases-detail/:id',
        component: KnowledgeBasesDetailComponent,
        data: {
            functionCode: 'CONTENT_KNOWLEDGEBASE'
        },
        canActivate: [AuthGuard]
    }, 
    {
        path: 'categories',
        component: CategoriesComponent,
        data: {
            functionCode: 'CONTENT_CATEGORY'
        },
        canActivate: [AuthGuard]
    },
    {
        path: 'knowledge-bases/:knowledgeBasesId/comments',
        component: CommentsComponent,
        data: {
            functionCode: 'CONTENT_COMMENT'
        },
        canActivate: [AuthGuard]
    },
    {
        path: 'knowledge-bases/comments',
        component: CommentsComponent,
        data: {
            functionCode: 'CONTENT_COMMENT'
        },
        canActivate: [AuthGuard]
    }, 
    {
        path: 'knowledge-bases/reports',
        component: ReportsComponent,
        data: {
            functionCode: 'CONTENT_REPORT'
        },
        canActivate: [AuthGuard]
    },
    {
        path: 'knowledge-bases/:knowledgeBasesId/reports',
        component: ReportsComponent,
        data: {
            functionCode: 'CONTENT_REPORT'
        },
        canActivate: [AuthGuard]
    }, 
    {
        path: 'sogd',
        component: SogdComponent,
        data: {
            functionCode: 'CONTENT_SOGD'
        },
        canActivate: [AuthGuard]
    }, 
    {
        path: 'phonggd',
        component: PhonggdComponent,
        data: {
            functionCode: 'CONTENT_PHONGGD'
        },
        canActivate: [AuthGuard]
    }, 
    {
        path: 'truong',
        component: TruongComponent,
        data: {
            functionCode: 'CONTENT_TRUONG'
        },
        canActivate: [AuthGuard]
    }, 
    {
        path: 'lop',
        component: LopComponent,
        data: {
            functionCode: 'CONTENT_LOP'
        },
        canActivate: [AuthGuard]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ContentsRoutingModule {}
