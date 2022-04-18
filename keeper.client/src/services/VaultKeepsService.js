import { api } from "./AxiosService";

class VaultKeepsService {

  create(data) {
    const res = await api.post('api/vaultkeeps', data)
  }
}

export const vaultkeepsService = new VaultKeepsService();