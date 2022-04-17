import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import Pop from "../utils/Pop";
import { api } from "./AxiosService";

class KeepsService {

  async getAll() {
    try {
      const res = await api.get('api/keeps')
      logger.log('get all keeps', res.data)
      AppState.keeps = res.data
    } catch (error) {
      logger.error(error)
      Pop.toast(error.message, 'error')
    }
  }

  async getById(id) {
    try {
      const res = await api.get('api/keeps/' + id)
      logger.log('get by id', res.data)
      AppState.activeKeep = res.data
    } catch (error) {
      logger.error(error)
      Pop.toast(error.message, 'error')
    }
  }
}

export const keepsService = new KeepsService();