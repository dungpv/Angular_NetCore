import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { DanhMucService, SogdService, PhonggdService, NotificationService, UtilitiesService } from '@app/shared/services';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { MessageConstants, SystemConstants } from '@app/shared/constants';
import { SelectItem } from 'primeng/api/selectitem';
import { Sogd, DMTinh, Phonggd, DMHuyen } from '@app/shared/models';

@Component({
  selector: 'app-phonggd-detail',
  templateUrl: './phonggd-detail.component.html',
  styleUrls: ['./phonggd-detail.component.css']
})
export class PhonggdDetailComponent implements OnInit, OnDestroy {

  constructor(
    public bsModalRef: BsModalRef,
    private danhMucService: DanhMucService,
    private sogdService: SogdService,
    private phonggdService: PhonggdService,
    private notificationService: NotificationService,
    private utilitiesService: UtilitiesService,
    private fb: FormBuilder) {
  }

  private subscription = new Subscription();
  public entityForm: FormGroup;
  public dialogTitle: string;
  private savedEvent: EventEmitter<any> = new EventEmitter();
  public dmTinh: DMTinh[] = [];
  public dmHuyen: DMHuyen[] = [];
  public soGD: any[] = [];
  public entityId: number;
  public btnDisabled = false;

  public blockedPanel = false;

  // Validate
  validation_messages = {
    'ma': [
      { type: 'required', message: 'Trường này bắt buộc' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 20 kí tự' }
    ],
    'ten': [
      { type: 'required', message: 'Trường này bắt buộc' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 50 kí tự' }
    ],
    'maSoGD': [
      { type: 'required', message: 'Trường này bắt buộc' },
    ],     
    'maTinh': [
      { type: 'required', message: 'Trường này bắt buộc' },
    ], 
    'maHuyen': [
      { type: 'required', message: 'Trường này bắt buộc' },
    ],  
    'thuTu': [
      { type: 'required', message: 'Trường này bắt buộc' },
    ]
  };

  ngOnInit() {
    this.entityForm = this.fb.group({
      'ma': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(20)
      ])),
      'ten': new FormControl('', Validators.compose([
        Validators.required,
        Validators.maxLength(50)
      ])),
      'diaChi': new FormControl(''),
      'dienThoai': new FormControl(''),
      'email': new FormControl(''),
      'website': new FormControl(''),
      'thuTu': new FormControl(null, Validators.required),
      'maSoGD': new FormControl(null, Validators.required), 
      'maTinh': new FormControl(null, Validators.required),
      'maHuyen': new FormControl(null, Validators.required)
    });
    this.subscription.add(
      this.sogdService.GetSoGD()
      .subscribe((response: Sogd[]) => {
        response.forEach(element => {
          this.soGD.push({
            value: element.ma,
            label: element.ten
          });
        });
        if (this.entityId) {
          this.dialogTitle = 'Cập nhật';
          this.danhMucService.getDMHuyenAll().subscribe(dmHuyen => {
            this.dmHuyen = dmHuyen;
          });
          this.loadFormDetails(this.entityId);
        } else {
          this.dialogTitle = 'Thêm mới';
        }
      }));
      this.danhMucService.getDMTinh().subscribe(dmTinh => {
        this.dmTinh = dmTinh;
      });
  }

  getHuyenByTinh(event)
  {
    this.danhMucService.getDMHuyen(event.target.value).subscribe(dmHuyen => this.dmHuyen = dmHuyen);
  }
  private loadFormDetails(id: any) {
    this.blockedPanel = true;
    this.subscription.add(this.phonggdService.getDetail(id).subscribe((response: any) => {
      this.entityForm.setValue({
        ma: response.ma,
        ten: response.ten,
        maSoGD: response.maSoGD,
        diaChi: response.diaChi,
        dienThoai: response.dienThoai,
        email: response.email,
        website: response.website,
        thuTu: response.thuTu,
        maTinh: response.maTinh,
        maHuyen: response.maHuyen,
      });
      setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
    }, error => {
      setTimeout(() => { this.blockedPanel = false; this.btnDisabled = false; }, 1000);
    }));
  }


  public saveChange() {
    this.btnDisabled = true;
    this.blockedPanel = true;
    if (this.entityId) {
      this.subscription.add(this.phonggdService.update(this.entityId, this.entityForm.getRawValue())
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
      this.subscription.add(this.phonggdService.add(this.entityForm.getRawValue())
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
