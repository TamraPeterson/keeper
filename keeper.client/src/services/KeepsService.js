import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { api } from "./AxiosService";

class KeepsService {

  async getAll() {

    const res = await api.get('api/keeps')
    logger.log('get all keeps', res.data)
    AppState.keeps = res.data

  }

  async getById(id) {

    const res = await api.get('api/keeps/' + id)
    logger.log('get by id', res.data)
    AppState.activeKeep = res.data

  }

  async create(data) {
    data.creator = AppState.account
    data.creatorId = AppState.account.id
    const res = await api.post('api/keeps', data)
    logger.log('create keep', res.data)

  }

  async remove(id) {
    const res = await api.delete('api/keeps/' + id)
    logger.log('delete keep', res.data)
  }
}

export const keepsService = new KeepsService();