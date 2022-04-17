import { AppState } from "../AppState";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class ProfilesService {

  async getProfile(id) {
    const res = await api.get('api/profiles/' + id)
    logger.log('get profile', res.data)
    AppState.profile = res.data
  }

  async getKeepsByProfile(id) {
    const res = await api.get(`api/profiles/${id}/keeps`)
    logger.log('get keeps for profile', res.data)
    AppState.keeps = res.data
  }

  async getVaultsByProfile(id) {
    const res = await api.get(`api/profiles/${id}/vaults`)
    logger.log('get vaults for profile', res.data)
    AppState.vaults = res.data
  }
}

export const profilesService = new ProfilesService();