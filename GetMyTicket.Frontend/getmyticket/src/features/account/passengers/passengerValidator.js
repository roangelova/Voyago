import { object, string, boolean,} from "yup";

export const createPassengerSchema = object({
  firstName: string().required().max(30),
  lastName: string().required().max(30),
  gender: string().required(),
  documentType: string().nullable(),
  documentId: string().nullable().max(20),
  nationality: string().nullable().max(20),
 // documentExpirationDate: date().nullable(),
  isAccountOwber: boolean()
});
