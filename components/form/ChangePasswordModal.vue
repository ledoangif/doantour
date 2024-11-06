<!-- <template>
    <CVModal id_model="changePasswordModal">
        <template #icon>
            <slot name="icon"></slot>
        </template>
        <template #title>
            <slot name="header">Đổi mật khẩu</slot>
        </template>
        <template #body>
            <form @submit.prevent="handleSubmit()">
                <div v-if="!isAdminChangeMode" class="row mb-3 form-group required">
                    <label for="old-pass" class="col-sm-3 col-form-label control-label text-end">
                        Mật khẩu hiện tại
                    </label>
                    <div class="col-sm-9">
                        <input
                            type="password"
                            v-model="currentPassword"
                            @blur="validateOldPassword"
                            class="form-control"
                        />
                        <span v-if="errors.currentPassword" class="error-message text-danger">
                            {{ errors.currentPassword }}
                        </span>
                    </div>
                </div>
                <div class="row mb-3 form-group required">
                    <label for="new-pass" class="col-sm-3 col-form-label control-label text-end">Mật khẩu mới</label>
                    <div class="col-sm-9">
                        <input
                            type="password"
                            v-model="newPassword"
                            @blur="validatePassword"
                            @input="validatePassword"
                            class="form-control"
                        />
                        <span v-if="errors.newPassword" class="error-message text-danger">
                            {{ errors.newPassword }}
                        </span>
                    </div>
                </div>
                <div class="row mb-3 form-group required">
                    <label for="cf-pass" class="col-sm-3 col-form-label control-label text-end">
                        Xác nhận mật khẩu
                    </label>
                    <div class="col-sm-9">
                        <input
                            type="password"
                            v-model="confirmPassword"
                            @blur="validateConfirmPassword"
                            @input="validateConfirmPassword"
                            class="form-control"
                        />
                        <span v-if="errors.confirmPassword" class="error-message text-danger">
                            {{ errors.confirmPassword }}
                        </span>
                    </div>
                </div>
                <div class="modal-footer d-flex justify-content-center">
                    <button type="button" class="btn btn-secondary d-flex align-items-center" data-bs-dismiss="modal">
                        Huỷ bỏ
                    </button>
                    <button type="submit" class="btn btn-warning d-flex align-items-center">
                        Lưu
                    </button>
                </div>
            </form>
        </template>
    </CVModal>
</template>

<script>
import { defineComponent } from 'vue';
import Api from '~/service/Base/api';

const api = new Api();

export default defineComponent({
    name: 'ChangePasswordModal',
    props: {
        accountId: {
            type: String,
            required: true,
        },
        isAdminChangeMode: {
            type: Boolean,
            required: false,
            default: false,
        },
    },
    data() {
        return {
            currentPassword: '',
            newPassword: '',
            confirmPassword: '',
            errors: {
                currentPassword: '',
                newPassword: '',
                confirmPassword: '',
            }
        };
    },
    methods: {
        validateOldPassword() {
            if (!this.isAdminChangeMode && this.currentPassword.trim().length === 0) {
                this.errors.currentPassword = 'Mật khẩu hiện tại không được để trống.';
            } else {
                this.errors.currentPassword = '';
            }
        },
        validatePassword() {
            if (this.newPassword.trim().length === 0) {
                this.errors.newPassword = 'Mật khẩu mới không được để trống.';
                return false;
            }
            if (this.newPassword === this.currentPassword) {
                this.errors.newPassword = 'Mật khẩu mới không được trùng với mật khẩu hiện tại.';
                return false;
            } else if (this.newPassword.length < 5) {
                this.errors.newPassword = 'Mật khẩu mới phải chứa ít nhất 5 ký tự.';
                return false;
            }

            const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{5,}$/;
            if (!passwordRegex.test(this.newPassword)) {
                this.errors.newPassword = 'Mật khẩu phải có ít nhất 5 ký tự gồm chữ hoa, chữ thường và số';
                return false;
            }

            this.errors.newPassword = ''; // Xóa thông báo lỗi nếu mật khẩu hợp lệ
            return true;
        },
        validateConfirmPassword() {
            if (this.confirmPassword.trim().length === 0) {
                this.errors.confirmPassword = 'Xác nhận mật khẩu không được để trống.';
                return false;
            }
            if (this.newPassword !== this.confirmPassword) {
                this.errors.confirmPassword = 'Xác nhận mật khẩu không khớp với mật khẩu mới.';
                return false;
            }
            this.errors.confirmPassword = '';
            return true;
        },
        validateInputs() {
            this.validateOldPassword();
            this.validatePassword();
            this.validateConfirmPassword();

            return (
                !this.errors.currentPassword &&
                !this.errors.newPassword &&
                !this.errors.confirmPassword
            );
        },
        handleSubmit() {
            if (this.validateInputs()) {
                if (!this.isAdminChangeMode) {
                    api.putAPI(`/Account/changePassword?accountId=${this.accountId}&oldPassword=${this.currentPassword}&newPassword=${this.newPassword}`)
                        .then((response) => {
                            if (response.status === 200) {  // Kiểm tra mã trạng thái
                                localStorage.removeItem('token');
                                navigateTo('/Login');
                                $('#changePasswordModal').modal('hide');
                                $('.btn-close').click();
                                this.currentPassword = '';
                                this.newPassword = '';
                                this.confirmPassword = '';
                            } else {
                                // Hiển thị thông báo lỗi từ backend nếu có
                                this.errors.currentPassword = response.data.Message || 'Mật khẩu hiện tại không chính xác.';
                            }
                        })
                } else {
                    // Xử lý đổi mật khẩu cho admin
                    api.putAPI(
                        `/Account/editPassword/${this.accountId}?password=${this.newPassword}`,
                        {},
                    )
                        .then((response) => {
                            $('#changePasswordModal').modal('hide');
                            $('.btn-close').click();
                            this.newPassword = '';
                            this.confirmPassword = '';
                        })
                        .catch((err) => {
                            console.log(err);
                        });
                    
                }
            }
        },
    },
});
</script> -->
<template>
    <CVModal id_model="changePasswordModal">
      <template #icon>
        <slot name="icon"></slot>
      </template>
      <template #title>
        <slot name="header">Đổi mật khẩu</slot>
      </template>
      <template #body>
        <form @submit.prevent="handleSubmit">
          <div v-if="!isAdminChangeMode" class="row mb-3 form-group required">
            <label for="old-pass" class="col-sm-3 col-form-label control-label text-end">
              Mật khẩu hiện tại
            </label>
            <div class="col-sm-9">
              <input
                type="password"
                v-model="currentPassword"
                @blur="validateOldPassword"
                class="form-control"
              />
              <span v-if="errors.currentPassword" class="error-message text-danger">
                {{ errors.currentPassword }}
              </span>
            </div>
          </div>
          <div class="row mb-3 form-group required">
            <label for="new-pass" class="col-sm-3 col-form-label control-label text-end">Mật khẩu mới</label>
            <div class="col-sm-9">
              <input
                type="password"
                v-model="newPassword"
                @blur="validatePassword"
                @input="validatePassword"
                class="form-control"
              />
              <span v-if="errors.newPassword" class="error-message text-danger">
                {{ errors.newPassword }}
              </span>
            </div>
          </div>
          <div class="row mb-3 form-group required">
            <label for="cf-pass" class="col-sm-3 col-form-label control-label text-end">
              Xác nhận mật khẩu
            </label>
            <div class="col-sm-9">
              <input
                type="password"
                v-model="confirmPassword"
                @blur="validateConfirmPassword"
                @input="validateConfirmPassword"
                class="form-control"
              />
              <span v-if="errors.confirmPassword" class="error-message text-danger">
                {{ errors.confirmPassword }}
              </span>
            </div>
          </div>
          <div class="modal-footer d-flex justify-content-center">
            <button type="button" class="btn btn-secondary d-flex align-items-center" data-bs-dismiss="modal">
              Huỷ bỏ
            </button>
            <button type="submit" class="btn btn-warning d-flex align-items-center">
              Lưu
            </button>
          </div>
        </form>
      </template>
    </CVModal>
  </template>
  
  <script>
  import { ref, defineComponent } from 'vue';
  import Api from '~/service/Base/api';
  
  const api = new Api();
  
  export default defineComponent({
    name: 'ChangePasswordModal',
    props: {
      accountId: {
        type: String,
        required: true,
      },
      isAdminChangeMode: {
        type: Boolean,
        required: false,
        default: false,
      },
    },
    setup(props) {
      // Khai báo các biến reactive cho form và lỗi
      const currentPassword = ref('');
      const newPassword = ref('');
      const confirmPassword = ref('');
      const errors = ref({
        currentPassword: '',
        newPassword: '',
        confirmPassword: '',
      });
  
      // Hàm kiểm tra mật khẩu cũ
      const validateOldPassword = () => {
        if (!props.isAdminChangeMode && currentPassword.value.trim().length === 0) {
          errors.value.currentPassword = 'Mật khẩu hiện tại không được để trống.';
        } else {
          errors.value.currentPassword = '';
        }
      };
  
      // Hàm kiểm tra mật khẩu mới
      const validatePassword = () => {
        if (newPassword.value.trim().length === 0) {
          errors.value.newPassword = 'Mật khẩu mới không được để trống.';
          return false;
        }
        if (newPassword.value === currentPassword.value) {
          errors.value.newPassword = 'Mật khẩu mới không được trùng với mật khẩu hiện tại.';
          return false;
        } else if (newPassword.value.length < 5) {
          errors.value.newPassword = 'Mật khẩu mới phải chứa ít nhất 5 ký tự.';
          return false;
        }
  
        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{5,}$/;
        if (!passwordRegex.test(newPassword.value)) {
          errors.value.newPassword = 'Mật khẩu phải có ít nhất 5 ký tự gồm chữ hoa, chữ thường và số';
          return false;
        }
  
        errors.value.newPassword = ''; // Xóa thông báo lỗi nếu mật khẩu hợp lệ
        return true;
      };
  
      // Hàm kiểm tra mật khẩu xác nhận
      const validateConfirmPassword = () => {
        if (confirmPassword.value.trim().length === 0) {
          errors.value.confirmPassword = 'Xác nhận mật khẩu không được để trống.';
          return false;
        }
        if (newPassword.value !== confirmPassword.value) {
          errors.value.confirmPassword = 'Xác nhận mật khẩu không khớp với mật khẩu mới.';
          return false;
        }
        errors.value.confirmPassword = '';
        return true;
      };
  
      // Hàm kiểm tra tất cả các input
      const validateInputs = () => {
        validateOldPassword();
        validatePassword();
        validateConfirmPassword();
  
        return (
          !errors.value.currentPassword &&
          !errors.value.newPassword &&
          !errors.value.confirmPassword
        );
      };
  
      // Hàm xử lý khi gửi form
      const handleSubmit = () => {
        if (validateInputs()) {
          if (!props.isAdminChangeMode) {
            api.putAPI(`/Account/changePassword?accountId=${props.accountId}&oldPassword=${currentPassword.value}&newPassword=${newPassword.value}`)
              .then((response) => {
                if (response.status === 200) {
                  localStorage.removeItem('token');
                  navigateTo('/Login');
                  $('#changePasswordModal').modal('hide');
                  $('.btn-close').click();
                  currentPassword.value = '';
                  newPassword.value = '';
                  confirmPassword.value = '';
                } else {
                  errors.value.currentPassword = response.data.Message || 'Mật khẩu hiện tại không chính xác.';
                }
              });
          } else {
            api.putAPI(`/Account/editPassword/${props.accountId}?password=${newPassword.value}`, {})
              .then((response) => {
                $('#changePasswordModal').modal('hide');
                $('.btn-close').click();
                newPassword.value = '';
                confirmPassword.value = '';
              })
              .catch((err) => {
                console.log(err);
              });
          }
        }
      };
  
      // Trả các biến và hàm ra template
      return {
        currentPassword,
        newPassword,
        confirmPassword,
        errors,
        validateOldPassword,
        validatePassword,
        validateConfirmPassword,
        handleSubmit,
      };
    },
  });
  </script>
  