import { IBaseEntity } from "./IBaseEntity";
import { IPersonData } from "./IPersonData";
import { IRelationship } from "./IRelationship";

export interface IPersonInRelationshipCreate extends IBaseEntity {
    personId: number,
    person1Id: number,
    relationshipId: number,
}