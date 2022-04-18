<template>
  <div class="container-fluid d-flex">
    <div class="row main">
      <div class="col-md-6">
        <img class="image rounded pb-2" :src="activeKeep?.img" alt="" />
      </div>

      <div class="col-md-6">
        <div class="row justify-content-center align-items-center">
          <div class="col-2">
            <h5 class="m-2">
              <i class="mdi mdi-eye-outline text-primary" title="views"></i>
              {{ activeKeep.views }}
            </h5>
          </div>

          <div class="col-2">
            <h5
              class="
                m-2
                d-flex
                flex-row
                justify-content-center
                align-items-center
              "
            >
              <img class="icon me-2" src="src/assets/img/tinylogo.png" alt="" />
              {{ activeKeep?.kept }}
            </h5>
          </div>

          <div class="col-2">
            <h5 class="m-2">
              <i class="mdi mdi-share-variant text-primary" title="shares"></i>
              ?
            </h5>
          </div>

          <div class="row mt-md-5">
            <div class="col-12 text-center">
              <h2>{{ activeKeep?.name }}</h2>
            </div>
          </div>

          <div class="row mt-md-5 justify-content-center" style="height: 350px">
            <div class="col-9">
              {{ activeKeep?.description }}
            </div>
          </div>

          <div class="row mt-auto">
            <div class="col-5 text-center">
              <div class="dropdown">
                <button
                  class="btn btn-outline-primary dropdown-toggle"
                  type="button"
                  id="dropdownMenuButton"
                  data-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  Add To Vault
                </button>
                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                  <a class="dropdown-item" href="#">Action</a>
                  <a class="dropdown-item" href="#">Another action</a>
                  <a class="dropdown-item" href="#">Something else here</a>
                </div>
              </div>
            </div>
            <div class="col-2">
              <h3>
                <i
                  class="mdi mdi-delete-outline text-primary selectable"
                  title="delete"
                  @click="remove(activeKeep.id)"
                ></i>
              </h3>
            </div>
            <div class="col-5 text-center d-flex flex-row align-items-center">
              <img
                class="avatar img-fluid me-2 selectable"
                :src="activeKeep.creator?.picture"
                alt=""
                @click="goTo(activeKeep.creatorId)"
              />
              {{ activeKeep.creator?.name }}
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
import { computed } from "@vue/reactivity"
import { AppState } from "../AppState"
import { useRoute, useRouter } from "vue-router"
import { Modal } from "bootstrap"
import { keepsService } from "../services/KeepsService"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
export default {
  props: {
    activeKeep: {
      type: Object,
      required: true,
    }
  },
  setup(props) {
    const router = useRouter();
    const route = useRoute();
    return {
      activeKeep: computed(() => AppState.activeKeep),
      route,
      goTo(creatorId) {
        router.push({ name: "Profile", params: { id: creatorId } })
        Modal.getOrCreateInstance(document.getElementById("keep-details")).hide();
      },
      remove(id) {
        try {
          keepsService.remove(id)
        } catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }

      }
    }
  }
}
</script>


<style lang="scss" scoped>
.debug .container,
.debug .container-fluid {
  outline: 2px double blue;
  outline-offset: -2px;
}

.debug .row {
  outline: 2px dashed red;
  outline-offset: -2px;
}

.debug [class*="col-"] {
  outline: 2px dotted forestgreen;
  outline-offset: -3px;
}

.image {
  height: 600px;
  width: 550px;
  object-fit: cover;
}
p {
  text-align: center;
}
.icon {
  // background: url("src/assets/img/tinylogo.png");
  height: 18px;
  width: 18px;
  display: block;
}
.main {
  // height: 80vh;
}
.avatar {
  height: 30px;
  width: 30px;
  border-radius: 50%;
}
</style>