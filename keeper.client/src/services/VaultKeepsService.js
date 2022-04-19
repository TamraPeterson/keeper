import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class VaultKeepsService {

  async create(data) {
    const res = await api.post('api/vaultkeeps', data)
    logger.log('creating vaultkeep', res.data)
    AppState.vaultKeeps.push(res.data)
  }
}

export const vaultKeepsService = new VaultKeepsService();