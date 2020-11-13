import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { DanhMucService, SogdService, LopService, TruongService, NotificationService, UtilitiesService } from '@app/shared/services';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MessageConstants, SystemConstants } from '@app/shared/constants';
import { SelectItem } from 'primeng/api/selectitem';
import { Sogd, Lop, Truong, BaseDanhMuc } from '@app/shared/models';

@Component({
  selector: 'app-lop-detail',
  templateUrl: './lop-detail.component.html',
  styleUrls: ['./lop-detail.component.css']
})
export class LopDetailComponent implements OnInit, OnDestroy {

  constructor(
    public bsModalRef: BsModalRef,
    private danhMucService: DanhMucService,
    private sogdService: SogdService,
    private truongService: TruongService,
    private lopService: LopService,
    private notificationService: NotificationService,
    private utilitiesService: UtilitiesService,
    private fb: FormBuilder) {
  }

  private subscription = new Subscription();
  public entityForm: FormGroup;
  public dialogTitle: string;
  private savedEvent: EventEmitter<any> = new EventEmitter();
  public soGD: any[] = [];
  public dmCapHoc: any[] = [];
  public dmKhoi: BaseDanhMuc[] = [];
  public truong: Truong[] = [];
  public lop: Lop;
  public dmSoBuoiHocTrenTuan: BaseDanhMuc[] = [];
  public baseDanhMuc: BaseDanhMuc[] = [];
  public entityId: number;
  public maCapHoc: string;
  public maSoGD: string;
  public btnDisabled = false;

  public blockedPanel = false;

  // Validate
  validation_messages = {
    'ten': [
      { type: 'required', message: 'Trường này bắt buộc' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 50 kí tự' }
    ],
    'maSoGD': [
      { type: 'required', message: 'Trường này bắt buộc' },
    ],      
    'thuTu': [
      { type: 'required', message: 'Trường này bắt buộc' },
    ],  
    'maTruong': [
      { type: 'required', message: 'Trường này bắt buộc' },
    ],  
    'maKhoi': [
      { type: 'required', message: 'Trường này bắt buộc' },
    ],  
    'maCapHoc': [
      { type: 'required', message: 'Trường này bắt buộc' },
    ],  
    'maSoBuoiHocTrenTuan': [
      { type: 'required', message: 'Trường này bắt buộc' },
    ],  
  };

  ngOnInit() {
    this.entityForm = this.fb.group({
      'ten': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(50)
      ])),
      'thuTu': new FormControl(null, Validators.required),
      'maSoGD': new FormControl(null, Validators.required), 
      'maTruong': new FormControl(null, Validators.required),
      'maKhoi': new FormControl(null, Validators.required),
      'maCapHoc': new FormControl('', Validators.required),
      'maSoBuoiHocTrenTuan': new FormControl(null, Validators.required),
      'trangThai': new FormControl(''),
      'isLopGhep': new FormControl(''),
    });

    if (this.entityId) {
      this.dialogTitle = 'Cập nhật';
      this.loadFormDetails(this.entityId);
    } else {
      this.dialogTitle = 'Thêm mới';
    }
      this.sogdService.GetSoGD()
      .subscribe((response: Sogd[]) => {
        response.forEach(element => {
          this.soGD.push({
            value: element.ma,
            label: element.ten
          });
        });
      });
      this.danhMucService.getDMKhoi(this.maCapHoc).subscribe(dmKhoi => {
        this.dmKhoi = dmKhoi;
      }); 
      this.danhMucService.getDMSoBuoiHocTrenTuan().subscribe(dmSoBuoiHocTrenTuan => {
        this.dmSoBuoiHocTrenTuan = dmSoBuoiHocTrenTuan;
      }); 
      this.danhMucService.getDMCapHoc().subscribe(dmCapHoc => {
        this.dmCapHoc = dmCapHoc;
      }); 
   
  }

  getDanhMucBySoGD(event)
  {
    this.truongService.getTruongBySoGDByNamHoc(event.target.value, this.entityForm.get('maCapHoc').value)
                      .subscribe(truong => this.truong = truong);
  }
  getDanhMucByCapHoc(event)
  {
    this.danhMucService.getDMKhoi(event.target.value).subscribe(dmKhoi => this.dmKhoi = dmKhoi);
    this.truongService.getTruongBySoGDByNamHoc(this.entityForm.get('maSoGD').value, event.target.value)
                      .subscribe(truong => this.truong = truong);
  }
  private loadFormDetails(id: any) {
    this.blockedPanel = true;
    this.subscription.add(this.lopService.getDetail(id).subscribe((response: Lop) => {
      this.entityForm.setValue({
        ten: response.ten,
        maSoGD: response.maSoGD,
        thuTu: response.thuTu,
        maCapHoc: response.maCapHoc,
        maKhoi: response.maKhoi,
        maSoBuoiHocTrenTuan: response.maSoBuoiHocTrenTuan,
        maTruong: response.maTruong,
        trangThai: response.trangThai,
        isLopGhep: response.isLopGhep,
      });
      setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
    }, error => {
      setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
    }));  
  }

  public saveChange() {
    this.btnDisabled = true;
    this.blockedPanel = true;
    this.entityForm.value.trangThai = this.entityForm.value.trangThai ? 1 : 0;
    this.entityForm.value.isLopGhep = this.entityForm.value.isLayDiem ? 1 : 0;
    if (this.entityId) {
      this.subscription.add(this.lopService.update(this.entityId, this.entityForm.value)
        .subscribe(() => {
          this.savedEvent.emit(this.entityForm.value);
          this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
          this.btnDisabled = false;
          setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
        }, error => {
          this.notificationService.showError(MessageConstants.UPDATED_NOT_OK_MSG);
          setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
        }));
    } else {
      this.subscription.add(this.lopService.add(this.entityForm.value)
        .subscribe(() => {
          this.savedEvent.emit(this.entityForm.value);
          this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
          this.btnDisabled = false;
          setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
        }, error => {
          this.notificationService.showError(MessageConstants.CREATED_NOT_OK_MSG);
          setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
        }));
    }
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
