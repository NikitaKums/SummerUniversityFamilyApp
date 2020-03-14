import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IPersonInRelationship} from "../interfaces/IPersonInRelationShip";
import { IPersonInRelationshipCreate } from "interfaces/IPersonInRelationshipCreate";
import { IPersonInRelationshipEdit } from "interfaces/IPersonInRelationshipEdit";
import { IPersonData } from "interfaces/IPersonData";

export var log = LogManager.getLogger('PersonInRelationShipsService');

@autoinject
export class PersonInRelationshipService extends BaseService<IPersonInRelationship> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'PersonInRelationships');
  }

  fetchYoungestAunt(): Promise<IPersonData>{
    let url = this.appConfig.apiUrl + 'PersonInRelationships/GetYoungestAunt';

    return this.httpClient.fetch(url)
      .then(response => {
        log.debug('response', response);
        return response.json();
      })
      .then(jsonData => {
        log.debug('jsonData', jsonData);
        return jsonData;
      }).catch(reason => {
        log.debug('catch reason', reason);
      });
  }

  fetchYoungestUncle(): Promise<IPersonData> {
    let url = this.appConfig.apiUrl + 'PersonInRelationships/GetYoungestUncle';

    return this.httpClient.fetch(url)
      .then(response => {
        log.debug('response', response);
        return response.json();
      })
      .then(jsonData => {
        log.debug('jsonData', jsonData);
        return jsonData;
      }).catch(reason => {
        log.debug('catch reason', reason);
      });
  }

  postAddPersonToRelationship(entity: IPersonInRelationshipCreate): Promise<Response> {
    let url = this.appConfig.apiUrl + 'PersonInRelationships';

    return this.httpClient.post(url, JSON.stringify(entity)).then(
      response => {
        log.debug('response', response);
        return response;
      }
    );
  }

  // update entity
  putCustom(entity: IPersonInRelationshipCreate): Promise<Response> {
    let url = this.appConfig.apiUrl + 'PersonInRelationships/' + entity.id;

    return this.httpClient.put(url, JSON.stringify(entity)).then(
      response => {
        log.debug('response', response);
        return response;
      }
    );

  }

    // get single entity for editing
    fetchForEdit(id: number): Promise<IPersonInRelationshipEdit> {
      let url = this.appConfig.apiUrl + 'PersonInRelationships/' + id;
      return this.httpClient.fetch(url)
        .then(response => {
          log.debug('response', response);
          return response.json();
        })
        .then(jsonData => {
          log.debug('jsonData', jsonData);
          return jsonData;
        }).catch(reason => {
          log.debug('catch reason', reason);
        });
    }
}