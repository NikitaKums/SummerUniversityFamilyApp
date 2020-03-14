import {LogManager, autoinject} from "aurelia-framework";
import {HttpClient} from "aurelia-fetch-client";
import {BaseService} from "./base-service";
import {AppConfig} from "../app-config";
import {IRelationship} from "../interfaces/IRelationship";

export var log = LogManager.getLogger('RelationshipsService');

@autoinject
export class RelationshipService extends BaseService<IRelationship> {

  constructor(
    private httpClient: HttpClient,
    private appConfig: AppConfig
  ) {
    super(httpClient, appConfig, 'Relationships');
  }

}