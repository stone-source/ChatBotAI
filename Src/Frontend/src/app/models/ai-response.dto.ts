import { UserRequestDetailsDto } from './user-request-details.dto';

export interface AiEngineResponseDetailsDto {
    id: string;
    userRequestId: string;
    userRequest: UserRequestDetailsDto | null;
    answer: string | null;
    rating: boolean | null;
    createdDateTime: Date | null;
    modifiedDateTime: Date | null;
}