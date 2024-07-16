import { Injectable } from '@angular/core';
import { ToastInfo } from '../models/toastInfo';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  toasts: ToastInfo[] = [];

  showDanger(header: string, body: string) {
    this.toasts.push({ header, body, classname: "bg-danger text-light" });
  }

  remove(toast: ToastInfo) {
    this.toasts = this.toasts.filter(t => t != toast);
  }
}
