import { HttpParams } from '@angular/common/http';
import { CustomQueryParams } from '../../models/customQueryParams';
import { environment } from '../../../environments/environment';

export class BaseApi {
  readonly baseUrl = environment.apiUrl;
  
  addQueryParams<T extends CustomQueryParams>(data: T): HttpParams {
    let params = new HttpParams();

    const keys = Object.keys(data);
    for (const key of keys) {
      let keyVal = data[key];

      if (!keyVal) continue;

      if (Array.isArray(keyVal)) {
        keyVal.forEach((el) => {
          params = params.append(key, el);
        });
        continue;
      }

      params = params.append(key, keyVal);
    }

    return params;
  }
}
