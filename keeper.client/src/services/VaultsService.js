import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class VaultsService {

  async getById(id) {
    const res = await api.get('api/vaults/' + id)
    logger.log('get vault', res.data)
    AppState.activeVault = res.data
  }

  async create(data) {
    if (data.isPrivate == "false") {
      data.isPrivate = false
    } else { data.isPrivate = true }
    data.creator = AppState.account
    data.creatorId = AppState.account.id

    const res = await api.post('api/vaults', data)
    logger.log('create vault', res.data)
    AppState.vaults.push(res.data)
    this.getById(data.id)
    return res.data

  }
}

export const vaultsService = new VaultsService();