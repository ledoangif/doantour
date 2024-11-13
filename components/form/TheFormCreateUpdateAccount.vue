<template>
    <CVModal :id_model="id_modal" :data_bs_target_modal="data_bs_target_modal">
        <template #icon>
            <slot name="icon"></slot>
        </template>
        <template #title>
            <span v-if="!isEditMode">
                <slot name="header">Thêm tài khoản</slot>
            </span>
            <span v-else>
                <slot name="header">Cập nhật tài khoản</slot>
            </span>
        </template>
        <template #body>
            <Form @submit="handleSubmit">
                <div class="row mb-3 form-group required">
                    <label
                        for="email"
                        class="col-sm-2 col-form-label control-label text-end"
                        >Email</label
                    >
                    <div class="col-sm-9">
                        <Field
                            type="text"
                            v-model="account.email"
                            name="email"
                            class="form-control"
                            :rules="{ required: true, email: true }"
                        />
                        <ErrorMessage name="email" class="text-danger" />
                        <span v-if="emailError" class="text-danger">{{ emailError }}</span>
                    </div>
                </div>

                <div v-if="!isEditMode" class="row mb-3 form-group required">
                    <label
                        for="password"
                        class="col-sm-2 col-form-label control-label text-end"
                    >
                        Mật khẩu
                    </label>
                    <div class="col-sm-9">
                        <Field
                            name="password"
                            :rules="{ required: true, min: 5, password: true }"
                            v-model="account.password"
                            type="password"
                            class="form-control"
                        />
                        <ErrorMessage name="password" class="text-danger" />
                    </div>
                </div>
                <div class="row mb-3 form-group required">
                    <label
                        for="fullName"
                        class="col-sm-2 col-form-label control-label text-end"
                    >
                        Họ tên
                    </label>
                    <div class="col-sm-9">
                        <Field
                            name="fullName"
                            v-model="account.fullName"
                            type="text"
                            class="form-control"
                            :rules="{ required: true, onlyCharacters: true }"
                        />
                        <ErrorMessage name="fullName" class="text-danger" />
                    </div>
                </div>
                <div class="row mb-3 form-group required">
                    <label
                        for="phoneNumber"
                        class="col-sm-2 col-form-label control-label text-end"
                        >Điện thoại</label>
                    <div class="col-sm-9">
                        <Field
                            name="phoneNumber"
                            :rules="{ required: true, phone: true }"
                            v-model="account.phone"
                            type="text"
                            class="form-control"
                        />
                        <ErrorMessage name="phoneNumber" class="text-danger" />
                    </div>
                </div>

                <div class="row mb-3 form-group required">
                    <label
                        for="authority"
                        class="col-sm-2 col-form-label control-label text-end"
                        >Quyền</label
                    >
                    <div class="col-sm-9">
                        <Field
                            as="select"
                            name="roleId"
                            v-model="account.roleId"
                            class="form-select"
                        >
                            <option selected disabled>Select Role</option>
                            <option v-for="role in roles" :value="role.id">
                                {{ role.roleName }}
                            </option>
                        </Field>
                    </div>
                </div>
                <div class="modal-footer align-content-center justify-content-center">
                    <button type="submit" class="btn btn-primary">
                        <i class="fa-solid fa-floppy-disk mx-1"></i>
                        Lưu
                    </button>
                    <button
                        type="button"
                        class="btn btn-outline-primary cancel-btn"
                        data-bs-dismiss="modal"
                    >
                        <i class="fa-solid fa-arrow-left mx-1"></i>
                        Huỷ bỏ
                    </button>
                </div>
            </Form>
        </template>
    </CVModal>
</template>

<script>
import Api from '~/service/Base/api.ts';
import { configure, defineRule, Field, Form, ErrorMessage } from 'vee-validate';

const api = new Api();

export default {
    name: 'CreateUpdateAccountModal',
    props: {
        editAccount: {
            type: Object,
            default: null,
            required: false,
        },
        isEditMode: {
            type: Boolean,
            default: false,
            required: true,
        },
        data_bs_target_modal: {
            type: String,
            default: '',
        },
        id_modal: {
            type: String,
            default: '',
        },
        roles: {
            type: Array,
            default: null,
            required: false,
        },
    },
    data() {
        return {
            account: {
                id: '',
                email: null,
                password: null,
                fullName: null,
                phone: null,
                roleId: 2,
            },
            emailError: "", // Thông báo lỗi email trùng
        };
    },
    watch: {
        /**
         * Watcher for the 'editAccount' property.
         * It's set to be immediate, so it runs on component mount, not just when 'editAccount' changes.
         * The handler function is called whenever 'editAccount' changes.
         * @param {Object} newVal - The new value of 'editAccount'.
         */
        editAccount: {
            immediate: true,
            handler(newVal) {
                if (newVal) {
                    this.account = { ...newVal };
                }
            },
        },
    },
    methods: {
        
        /**
         * Create a new account
         */
        // createAccount() {
        //     api.postAPI('/Account/register', this.account)
        //         .then((res) => {
        //             this.$emit('account-saved');
        //             $('.cancel-btn').click();
        //             $(`#${this.id_modal}`).modal('hide');
        //         })
        //         .catch((err) => {});
        // },
        async createAccount() {
        // Kiểm tra email trước
        try {
            const response = await api.get(`/Account/email`, {
                params: { email: this.account.email }
            });
            if (response.data.responseData) {
                this.emailError = "Email này đã tồn tại.";
                return; // Dừng lại nếu email đã tồn tại
            } else {
                this.emailError = ""; // Xóa thông báo lỗi nếu email không trùng
            }

            // Nếu email không trùng, tiến hành gọi API đăng ký
            await api.postAPI('/Account/register', this.account);
            this.$emit('account-saved'); // Phát sự kiện lưu thành công
            $('.cancel-btn').click();
            $(`#${this.id_modal}`).modal('hide'); // Ẩn modal
        } catch (err) {
            console.error(err);
        }
    },

        async updateAccount() {
        // Kiểm tra xem dữ liệu có thay đổi so với dữ liệu ban đầu không
        const isChanged = Object.keys(this.account).some(
            key => this.account[key] !== this.editAccount[key]
        );
        if (!isChanged) {
            //this.emailError = "Không có thay đổi nào được thực hiện.";
            return; // Không thực hiện cập nhật và không ẩn modal nếu không có thay đổi
        }
        //Kiểm tra email đã tồn tại chưa, nếu đã tồn tại thì báo lỗi
        try {
            const response = await api.get(`/Account/email`, {
                params: { email: this.account.email }
            });
            if (response.data.responseData) {
                this.emailError = "Email này đã tồn tại.";
                return; // Dừng lại nếu email đã tồn tại
            } else {
                this.emailError = ""; // Xóa thông báo lỗi nếu email không trùng
            }
            await api.putAPI(`/Account/${this.editAccount.id}`, this.account);
            this.$emit('account-saved');
            $('.cancel-btn').click();
            $(`#${this.id_modal}`).modal('hide'); // Ẩn modal sau khi cập nhật thành công
        } catch (err) {
            console.error(err);
        }
    },
        async getAccountByEmail() {
            try {
                const response = await api.get(`/Account/email`, {
                    params: { email: this.account.email }
                });
                if (response.data.responseData) {
                    this.emailError = "Email đã được sử dụng.";
                } else {
                    this.emailError = "";
                }
            } catch (err) {
                console.error(err);
            }
        },
        handleSubmit() {
            if (!this.isEditMode) {
                this.createAccount();
            } else {
                this.updateAccount();
            }
        },
        getRoles() {
            api.get(`/Role`, null)
                .then((res) => {
                    this.roles = res.data.responseData;
                })
                .catch((err) => console.log(err));
        },
        resetForm() {
            this.account = {
                id: '',
                email: null,
                password: null,
                fullName: null,
                phoneNumber: null,
                roleId: 2,
            };
        },
    },
    components: {
        Form,
        Field,
        ErrorMessage,
    },
    emits: {
        'account-saved': null,
    },
};
</script>
