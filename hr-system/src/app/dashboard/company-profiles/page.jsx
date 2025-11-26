"use client"

import { useState, useEffect } from "react"
import { Button } from "@/components/ui/button"
import { Card, CardHeader, CardTitle, CardDescription, CardContent } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import {
  getAllCompanyProfiles,
  getCompanyProfileById,
  createCompanyProfile,
  updateCompanyProfile,
  deleteCompanyProfile,
} from "@/lib/api"

export default function CompanyProfilesPage() {
  const [profiles, setProfiles] = useState([])
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState("")
  const [showForm, setShowForm] = useState(false)
  const [editingProfile, setEditingProfile] = useState(null)
  const [role, setRole] = useState("")
  const [formData, setFormData] = useState({
    nameEn: "",
    insuranceNumber: "",
    taxNumber: "",
    phoneNumber: "",
    faxNumber: "",
    email: "",
    webSite: "",
    address: "",
  })
  const [formErrors, setFormErrors] = useState({})

  useEffect(() => {
    if (typeof window !== "undefined") {
      const userRole = localStorage.getItem("role")
      setRole(userRole || "")
    }
    fetchProfiles()
  }, [])

  const fetchProfiles = async () => {
    try {
      setLoading(true)
      setError("")
      const response = await getAllCompanyProfiles()
      setProfiles(response.data || [])
    } catch (err) {
      console.error("Error fetching company profiles:", err)
      setError(
        err.response?.data?.message ||
        err.message ||
        "Failed to fetch company profiles"
      )
    } finally {
      setLoading(false)
    }
  }

  const handleInputChange = (e) => {
    const { name, value } = e.target
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }))
    // Clear error for this field
    if (formErrors[name]) {
      setFormErrors((prev) => {
        const newErrors = { ...prev }
        delete newErrors[name]
        return newErrors
      })
    }
  }

  const validateForm = () => {
    const errors = {}

    if (!formData.nameEn.trim()) {
      errors.nameEn = "Company name is required"
    } else if (formData.nameEn.length > 150) {
      errors.nameEn = "Company name must be 150 characters or less"
    }

    if (formData.insuranceNumber && !/^\d{10}$/.test(formData.insuranceNumber)) {
      errors.insuranceNumber = "Insurance number must be exactly 10 digits"
    }

    if (!formData.taxNumber.trim()) {
      errors.taxNumber = "Tax number is required"
    } else if (formData.taxNumber.length > 15) {
      errors.taxNumber = "Tax number must be 15 characters or less"
    }

    if (formData.email && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      errors.email = "Invalid email format"
    }

    if (formData.phoneNumber && !/^\+?\d{10,15}$/.test(formData.phoneNumber.replace(/[\s-]/g, ""))) {
      errors.phoneNumber = "Invalid phone number format"
    }

    setFormErrors(errors)
    return Object.keys(errors).length === 0
  }

  const handleSubmit = async (e) => {
    e.preventDefault()

    if (!validateForm()) {
      return
    }

    try {
      setError("")
      if (editingProfile) {
        // Update existing profile
        const updateData = {
          ...formData,
          companyProfileId: editingProfile.companyProfileId,
        }
        await updateCompanyProfile(editingProfile.companyProfileId, updateData)
      } else {
        // Create new profile
        await createCompanyProfile(formData)
      }

      // Reset form and close
      setFormData({
        nameEn: "",
        insuranceNumber: "",
        taxNumber: "",
        phoneNumber: "",
        faxNumber: "",
        email: "",
        webSite: "",
        address: "",
      })
      setEditingProfile(null)
      setShowForm(false)
      setFormErrors({})
      fetchProfiles()
    } catch (err) {
      console.error("Error saving company profile:", err)
      setError(
        err.response?.data?.message ||
        err.message ||
        `Failed to ${editingProfile ? "update" : "create"} company profile`
      )
    }
  }

  const handleEdit = async (id) => {
    try {
      const response = await getCompanyProfileById(id)
      const profile = response.data
      setEditingProfile(profile)
      setFormData({
        nameEn: profile.nameEn || "",
        insuranceNumber: profile.insuranceNumber || "",
        taxNumber: profile.taxNumber || "",
        phoneNumber: profile.phoneNumber || "",
        faxNumber: profile.faxNumber || "",
        email: profile.email || "",
        webSite: profile.webSite || "",
        address: profile.address || "",
      })
      setFormErrors({})
      setShowForm(true)
    } catch (err) {
      console.error("Error fetching company profile:", err)
      setError(
        err.response?.data?.message ||
        err.message ||
        "Failed to fetch company profile"
      )
    }
  }

  const handleDelete = async (id) => {
    if (!confirm("Are you sure you want to delete this company profile?")) {
      return
    }

    try {
      await deleteCompanyProfile(id)
      fetchProfiles()
    } catch (err) {
      console.error("Error deleting company profile:", err)
      setError(
        err.response?.data?.message ||
        err.message ||
        "Failed to delete company profile"
      )
    }
  }

  const handleNew = () => {
    setEditingProfile(null)
    setFormData({
      nameEn: "",
      insuranceNumber: "",
      taxNumber: "",
      phoneNumber: "",
      faxNumber: "",
      email: "",
      webSite: "",
      address: "",
    })
    setFormErrors({})
    setError("")
    setShowForm(true)
  }

  const isAdmin = role === "admin"
  const canView = role === "admin" || role === "HR"

  if (!canView) {
    return (
      <div className="container mx-auto p-6">
        <Card>
          <CardContent className="p-6">
            <p className="text-destructive">You don't have permission to view this page.</p>
          </CardContent>
        </Card>
      </div>
    )
  }

  return (
    <div className="max-w-7xl mx-auto space-y-6">
      <div className="flex justify-between items-center">
        <div>
          <h1 className="text-4xl font-bold bg-gradient-to-r from-blue-600 via-purple-600 to-blue-600 bg-clip-text text-transparent">
            Company Profiles
          </h1>
          <p className="text-gray-600 mt-2">Manage company profile information</p>
        </div>
        {isAdmin && (
          <Button 
            onClick={handleNew}
            className="bg-gradient-to-r from-blue-600 to-purple-600 hover:from-blue-700 hover:to-purple-700 text-white shadow-lg"
          >
            Create New Profile
          </Button>
        )}
      </div>

      {error && !showForm && (
        <div className="p-3 text-sm text-destructive bg-destructive/10 rounded-md border border-destructive/20">
          {error}
        </div>
      )}

      {showForm && (
        <Card>
          <CardHeader>
            <CardTitle>{editingProfile ? "Edit Company Profile" : "Create Company Profile"}</CardTitle>
            <CardDescription>
              {editingProfile
                ? "Update the company profile information"
                : "Enter the company profile information"}
            </CardDescription>
          </CardHeader>
          <CardContent>
            <form onSubmit={handleSubmit} className="space-y-4">
              {error && (
                <div className="p-3 text-sm text-destructive bg-destructive/10 rounded-md border border-destructive/20">
                  {error}
                </div>
              )}

              <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div className="space-y-2">
                  <Label htmlFor="nameEn">
                    Company Name (English) <span className="text-destructive">*</span>
                  </Label>
                  <Input
                    id="nameEn"
                    name="nameEn"
                    value={formData.nameEn}
                    onChange={handleInputChange}
                    required
                    maxLength={150}
                    placeholder="TechCorp Ltd"
                  />
                  {formErrors.nameEn && (
                    <p className="text-sm text-destructive">{formErrors.nameEn}</p>
                  )}
                </div>

                <div className="space-y-2">
                  <Label htmlFor="taxNumber">
                    Tax Number <span className="text-destructive">*</span>
                  </Label>
                  <Input
                    id="taxNumber"
                    name="taxNumber"
                    value={formData.taxNumber}
                    onChange={handleInputChange}
                    required
                    maxLength={15}
                    placeholder="TX987654"
                  />
                  {formErrors.taxNumber && (
                    <p className="text-sm text-destructive">{formErrors.taxNumber}</p>
                  )}
                </div>

                <div className="space-y-2">
                  <Label htmlFor="insuranceNumber">Insurance Number</Label>
                  <Input
                    id="insuranceNumber"
                    name="insuranceNumber"
                    value={formData.insuranceNumber}
                    onChange={handleInputChange}
                    maxLength={10}
                    placeholder="1234567890"
                  />
                  {formErrors.insuranceNumber && (
                    <p className="text-sm text-destructive">{formErrors.insuranceNumber}</p>
                  )}
                </div>

                <div className="space-y-2">
                  <Label htmlFor="phoneNumber">Phone Number</Label>
                  <Input
                    id="phoneNumber"
                    name="phoneNumber"
                    value={formData.phoneNumber}
                    onChange={handleInputChange}
                    placeholder="+201234567890"
                  />
                  {formErrors.phoneNumber && (
                    <p className="text-sm text-destructive">{formErrors.phoneNumber}</p>
                  )}
                </div>

                <div className="space-y-2">
                  <Label htmlFor="faxNumber">Fax Number</Label>
                  <Input
                    id="faxNumber"
                    name="faxNumber"
                    value={formData.faxNumber}
                    onChange={handleInputChange}
                    placeholder="0201234567"
                  />
                </div>

                <div className="space-y-2">
                  <Label htmlFor="email">Email</Label>
                  <Input
                    id="email"
                    name="email"
                    type="email"
                    value={formData.email}
                    onChange={handleInputChange}
                    placeholder="info@techcorp.com"
                  />
                  {formErrors.email && (
                    <p className="text-sm text-destructive">{formErrors.email}</p>
                  )}
                </div>

                <div className="space-y-2">
                  <Label htmlFor="webSite">Website</Label>
                  <Input
                    id="webSite"
                    name="webSite"
                    type="url"
                    value={formData.webSite}
                    onChange={handleInputChange}
                    placeholder="https://techcorp.com"
                  />
                </div>

                <div className="space-y-2 md:col-span-2">
                  <Label htmlFor="address">Address</Label>
                  <Input
                    id="address"
                    name="address"
                    value={formData.address}
                    onChange={handleInputChange}
                    placeholder="Cairo, Egypt"
                  />
                </div>
              </div>

              <div className="flex gap-2 justify-end">
                <Button
                  type="button"
                  variant="outline"
                  onClick={() => {
                    setShowForm(false)
                    setEditingProfile(null)
                    setFormErrors({})
                    setError("")
                  }}
                >
                  Cancel
                </Button>
                <Button type="submit">
                  {editingProfile ? "Update" : "Create"}
                </Button>
              </div>
            </form>
          </CardContent>
        </Card>
      )}

      <Card>
        <CardHeader>
          <CardTitle>Company Profiles List</CardTitle>
          <CardDescription>
            {profiles.length} company profile{profiles.length !== 1 ? "s" : ""} found
          </CardDescription>
        </CardHeader>
        <CardContent>
          {loading ? (
            <p className="text-center py-8 text-muted-foreground">Loading...</p>
          ) : profiles.length === 0 ? (
            <p className="text-center py-8 text-muted-foreground">
              No company profiles found
            </p>
          ) : (
            <div className="overflow-x-auto">
              <table className="w-full border-collapse">
                <thead>
                  <tr className="border-b">
                    <th className="text-left p-3 font-semibold">ID</th>
                    <th className="text-left p-3 font-semibold">Company Name</th>
                    <th className="text-left p-3 font-semibold">Tax Number</th>
                    <th className="text-left p-3 font-semibold">Email</th>
                    <th className="text-left p-3 font-semibold">Phone</th>
                    <th className="text-left p-3 font-semibold">Address</th>
                    {isAdmin && <th className="text-left p-3 font-semibold">Actions</th>}
                  </tr>
                </thead>
                <tbody>
                  {profiles.map((profile) => (
                    <tr key={profile.companyProfileId} className="border-b hover:bg-muted/50">
                      <td className="p-3">{profile.companyProfileId}</td>
                      <td className="p-3 font-medium">{profile.nameEn}</td>
                      <td className="p-3">{profile.taxNumber}</td>
                      <td className="p-3">{profile.email || "-"}</td>
                      <td className="p-3">{profile.phoneNumber || "-"}</td>
                      <td className="p-3">{profile.address || "-"}</td>
                      {isAdmin && (
                        <td className="p-3">
                          <div className="flex gap-2">
                            <Button
                              variant="outline"
                              size="sm"
                              onClick={() => handleEdit(profile.companyProfileId)}
                            >
                              Edit
                            </Button>
                            <Button
                              variant="destructive"
                              size="sm"
                              onClick={() => handleDelete(profile.companyProfileId)}
                            >
                              Delete
                            </Button>
                          </div>
                        </td>
                      )}
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </CardContent>
      </Card>
    </div>
  )
}

