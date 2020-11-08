import { Component, OnInit, OnDestroy } from '@angular/core';
import { MessageConstants } from '@app/shared/constants';
import { TruongService, SogdService, LopService, DanhMucService, NotificationService } from '@app/shared/services';
import { Pagination, Lop, Sogd, Truong, BaseDanhMuc } from '@app/shared/models';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { BaseComponent } from '@app/protected-zone/base/base.component';
import { LopDetailComponent } from './lop-detail/lop-detail.component';

@Component({
  selector: 'app-lop',
  templateUrl: './lop.component.html',
  styleUrls: ['./lop.component.css']
})
export class LopComponent extends BaseComponent implements OnInit, OnDestroy {

  private subscription = new Subscription();
  public screenTitle: string;
  // Default
  public bsModalRef: BsModalRef;
  public blockedPanel = false;
  public totalItems = 0;
  /**
   * Paging
   */
  public pageIndex = 1;
  public pageSize = 50;
  public pageDisplay = 20;
  public totalRecords: number;
  public keyword = '';
  public maSoGD = '';
  public maCapHoc = '';
  public maTruong = '';
  // Role
  public items: any[];
  public selectedItems = [];
  soGD: Sogd[];
  capHoc: BaseDanhMuc[];
  truong: Truong[];
  constructor(private lopService: LopService,
    private sogdService: SogdService,
    private truongService: TruongService,
    private danhMucService: DanhMucService,
    private notificationService: NotificationService,
    private modalService: BsModalService) {
    super('CONTENT_LOP');
  }

  ngOnInit(): void {
    super.ngOnInit();
    this.loadData();
    this.getSoGD();
    this.getCapHoc();
    this.getTruongBySoGDByCapHoc();
  }
  getSoGD(): void {
    this.sogdService.GetSoGD().subscribe(sogd => this.soGD = sogd);
  }
  getCapHoc(): void {
    this.danhMucService.getDMCapHoc().subscribe(caphoc => this.capHoc = caphoc);
  }  
  getTruongBySoGDByCapHoc(): void {
    this.truongService.getTruongBySoGDByNamHoc(this.soGD, this.capHoc).subscribe(truong => this.truong = truong);
  }   
  selectSoGD() {
    this.getTruongBySoGDByCapHoc();
    this.loadData();
  }
  selectCapHoc() {
    this.getTruongBySoGDByCapHoc();
    this.loadData();
  } 
  selectTruong() {
    this.loadData();
  }  
  loadData(selectedId = null) {
    this.blockedPanel = true;
    this.subscription.add(this.lopService.getAllPaging(this.keyword, this.pageIndex, this.pageSize, this.maSoGD, this.maTruong, this.maCapHoc)
      .subscribe((response: Pagination<Lop>) => {
        this.processLoadData(selectedId, response);
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }, error => {
        setTimeout(() => { this.blockedPanel = false; }, 1000);
      }));
  }
  private processLoadData(selectedId = null, response: Pagination<Lop>) {
    this.items = response.items;
    this.pageIndex = this.pageIndex;
    this.pageSize = this.pageSize;
    this.totalRecords = response.totalRecords;
    if (this.selectedItems.length === 0 && this.items.length > 0) {
      this.selectedItems.push(this.items[0]);
    }
    if (selectedId != null && this.items.length > 0) {
      this.selectedItems = this.items.filter(x => x.Ma === selectedId);
    }
  }
  pageChanged(event: any): void {
    this.pageIndex = event.page + 1;
    this.pageSize = event.rows;
    this.loadData();
  }

  showAddModal() {
    this.bsModalRef = this.modalService.show(LopDetailComponent,
      {
        class: 'modal-lg',
        backdrop: 'static'
      });
    this.bsModalRef.content.savedEvent.subscribe((response) => {
      this.bsModalRef.hide();
      this.loadData();
      this.selectedItems = [];
    });
  }
  showEditModal() {
    if (this.selectedItems.length === 0) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    const initialState = {
      entityId: this.selectedItems[0].id
    };
    this.bsModalRef = this.modalService.show(LopDetailComponent,
      {
        initialState: initialState,
        class: 'modal-lg',
        backdrop: 'static'
      });
    this.subscription.add(this.bsModalRef.content.savedEvent.subscribe((response) => {
      this.bsModalRef.hide();
      this.loadData(response.ma);
    }));
  }

  deleteItems() {
    const id = this.selectedItems[0].id;
    this.notificationService.showConfirmation(MessageConstants.CONFIRM_DELETE_MSG,
      () => this.deleteItemsConfirm(id));
  }
  deleteItemsConfirm(id) {
    this.blockedPanel = true;
    this.subscription.add(this.lopService.delete(id).subscribe(() => {
      this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
      this.loadData();
      this.selectedItems = [];
      setTimeout(() => { this.blockedPanel = false; }, 1000);
    }, error => {
      setTimeout(() => { this.blockedPanel = false; }, 1000);
    }));
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
